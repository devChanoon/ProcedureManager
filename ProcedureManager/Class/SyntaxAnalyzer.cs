using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcedureManager
{
    public class SyntaxAnalyzer
    {
        private class FindResult
        {
            public int index = -1;
            public string item = string.Empty;
            public string beforeString = string.Empty;
            public string afterString = string.Empty;

            public FindResult FindTextInAllContent(List<string> list, string content, int findStartIndex = 0)
            {
                string _content = content.Substring(findStartIndex).ToUpper();

                string name = string.Empty;
                int index = int.MaxValue;
                foreach (string item in list)
                {
                    int spaceIndex = _content.IndexOf($"{item.ToUpper()} ");
                    int tabIndex = _content.IndexOf($"{item.ToUpper()}\t");
                    int _index = -1;
                    if (spaceIndex != -1 && tabIndex != -1)
                        _index = spaceIndex < tabIndex ? spaceIndex : tabIndex;
                    else if (spaceIndex != -1 || tabIndex != -1)
                        _index = spaceIndex == -1 ? tabIndex : spaceIndex;

                    if (_index != -1 && index > _index)
                    { 
                        index = _index;
                        name = item;
                    }
                }

                if (index != int.MaxValue)
                {
                    this.index = index;
                    item = content.Substring(index + findStartIndex, name.Length);
                    beforeString = content.Substring(0, index + findStartIndex);
                    afterString = content.Substring(index + findStartIndex + name.Length);
                }

                return this;
            }

            public FindResult FindTextInFirstWord(List<string> list, string content)
            {
                string _content = content.TrimStart();
                int index = content.Length - _content.Length;
                int tabIndex = 0;
                foreach(char c in _content)
                {
                    if (c == '\t')
                        tabIndex++;
                    else
                        break;
                }

                index = index + tabIndex;
                _content = _content.Substring(tabIndex);

                foreach (string item in list)
                {
                    if (_content.Length >= item.Length && _content.Substring(0, item.Length).ToUpper() == item)
                    {
                        this.index = index;
                        this.item = _content.Substring(0, item.Length);
                        beforeString = content.Substring(0, index);
                        afterString = content.Substring(index + item.Length);
                        break;
                    }
                }

                return this;
            }

            public FindResult FindProcedureNameInFirstWord(string content)
            {
                string _content = content.TrimStart();

                this.index = content.Length - _content.Length;
                const string SCHEMA = "[DBO].";
                if (_content.Length > SCHEMA.Length && _content.Substring(0, SCHEMA.Length).ToUpper() == SCHEMA)
                {
                    this.index += 6;
                    _content = _content.Substring(SCHEMA.Length);
                }

                int index = _content.IndexOf("]");
                item = _content.Substring(0, index).Trim().Replace("[", "").Replace("]", "");
                this.index += index + 1;
                return this;
            }
        }


        public ProcedureList GetProcedureList(string content)
        {
            ProcedureList procedureList = new ProcedureList();

            List<string> syntaxList = new List<string>() { "CREATE", "ALTER", "DROP" };
            List<string> typeList = new List<string>() { "PROCEDURE", "FUNCTION", "PROC" };

            string _content = content;
            int syntaxStartIndex = -1;
            int findStartIndex = 0;

            do
            {
                FindResult syntax = new FindResult().FindTextInAllContent(syntaxList, _content, findStartIndex);
                syntaxStartIndex = syntax.index;
                if (syntax.index > -1)
                {
                    FindResult type = new FindResult().FindTextInFirstWord(typeList, syntax.afterString);
                    if (type.index > -1)
                    {
                        FindResult table = new FindResult().FindProcedureNameInFirstWord(type.afterString);


                        if (syntax.beforeString != string.Empty && procedureList.Count > 0 && string.IsNullOrEmpty(procedureList.GetQuery()))
                        {
                            
                            procedureList.SetQuery(syntax.beforeString);
                            _content = $"{syntax.item}{syntax.afterString}";
                            findStartIndex = 0;
                        }
                        else
                        {
                            findStartIndex = (syntax.index + syntax.item.Length + type.index + type.item.Length + table.index);
                            procedureList.AddName(table.item);
                        }
                    }
                    else
                        findStartIndex += syntax.index + syntax.item.Length;
                }
            }
            while (syntaxStartIndex > -1);

            if (procedureList.Count > 0 && _content != string.Empty)
                procedureList.SetQuery(_content);

            return procedureList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syntaxList"></param>
        /// <param name="content"></param>
        /// <returns>item1 > syntax index / item2 > after syntax / item3 > before syntax</returns>
        public Tuple<int, string, string> GetTargetSyntaxIndex(List<string> syntaxList, string content)
        {
            string _content = content.ToUpper();
            foreach (string syntax in syntaxList)
            {
                int index = _content.IndexOf(syntax.ToUpper());
                if (index > -1)
                {
                    Tuple<string, string> partInfo = RemoveSyntaxPart(content, syntax.Length, index);
                    return new Tuple<int, string, string>(index, partInfo.Item2, partInfo.Item1);
                }
            }

            return new Tuple<int, string, string>(-1, content, string.Empty);
        }

        public Tuple<int, string> CheckFirstWord(List<string> syntaxList, string content)
        {
            string _content = content.ToUpper();
            foreach (string syntax in syntaxList)
            {
                if (_content.Length >= syntax.Length && _content.Substring(0, syntax.Length) == syntax)
                {
                    Tuple<string, string> partInfo = RemoveSyntaxPart(content, syntax.Length);
                    return new Tuple<int, string>(0, partInfo.Item2);
                }
            }

            return new Tuple<int, string>(-1, content);
        }

        /// <summary>
        /// syntax 이전 / 이후 부분 분리
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startIndex"></param>
        /// <returns>item1 = before syntax, item2 = after syntax</returns>
        private Tuple<string, string> RemoveSyntaxPart(string content, int syntaxLength, int startIndex = 0)
        {
            string beforeSyntax = content.Substring(0, startIndex);

            string _content = content.Substring(startIndex + syntaxLength);
            string targetText = _content.Substring(0, 1) == "\t" ? "\t" : " ";
            int spaceIndex = _content.IndexOf(targetText);
            string afterSyntax = _content.Substring(spaceIndex + 1).Trim();
            return new Tuple<string, string>(beforeSyntax, afterSyntax);
        }
    }
}
