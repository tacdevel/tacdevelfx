using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

internal partial class Program
{
    // http://jake.ginnivan.net/c-sharp-argument-parser/
    private class Arguments
    {
        private static readonly string cmdLine = Environment.CommandLine;

        public static string[] SplitCommandLine()
        {
            StringBuilder translatedArguments = new StringBuilder(cmdLine);
            bool escaped = false;
            for (int i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                    escaped = !escaped;
                if (translatedArguments[i] == ' ' && !escaped)
                    translatedArguments[i] = '\n';
            }

            string[] toReturn = translatedArguments.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < toReturn.Length; i++)
                toReturn[i] = RemoveMatchingQuotes(toReturn[i]);
            return toReturn;
        }

        public static string RemoveMatchingQuotes(string stringToTrim)
        {
            int firstQuoteIndex = stringToTrim.IndexOf('"');
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); //-1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }

            return stringToTrim;
        }

        private readonly Dictionary<string, Collection<string>> _parameters;
        private string _waitingParameter;

        public Arguments(IEnumerable<string> arguments)
        {
            _parameters = new Dictionary<string, Collection<string>>();

            string[] parts;

            //Splits on beginning of arguments ( - and -- and / )
            //And on assignment operators ( = and : )
            Regex argumentSplitter = new Regex(@"^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (string argument in arguments)
            {
                parts = argumentSplitter.Split(argument, 3);
                switch (parts.Length)
                {
                    case 1:
                        AddValueToWaitingArgument(parts[0]);
                        break;
                    case 2:
                        AddWaitingArgumentAsFlag();
                        //Because of the split index 0 will be a empty string
                        _waitingParameter = parts[1];
                        break;
                    case 3:
                        AddWaitingArgumentAsFlag();
                        //Because of the split index 0 will be a empty string
                        string valuesWithoutQuotes = RemoveMatchingQuotes(parts[2]);
                        AddListValues(parts[1], valuesWithoutQuotes.Split(','));
                        break;
                }
            }
            AddWaitingArgumentAsFlag();
        }

        private void AddListValues(string argument, IEnumerable<string> values)
        {
            foreach (string listValue in values)
                Add(argument, listValue);
        }

        private void AddWaitingArgumentAsFlag()
        {
            if (_waitingParameter == null) return;

            AddSingle(_waitingParameter, "true");
            _waitingParameter = null;
        }

        private void AddValueToWaitingArgument(string value)
        {
            if (_waitingParameter == null) return;

            value = RemoveMatchingQuotes(value);
            Add(_waitingParameter, value);
            _waitingParameter = null;
        }

        public int Count => _parameters.Count;

        public void Add(string argument, string value)
        {
            if (!_parameters.ContainsKey(argument))
                _parameters.Add(argument, new Collection<string>());

            _parameters[argument].Add(value);
        }

        public void AddSingle(string argument, string value)
        {
            if (!_parameters.ContainsKey(argument))
                _parameters.Add(argument, new Collection<string>());
            else
                throw new ArgumentException(string.Format("Argument {0} has already been defined", argument));

            _parameters[argument].Add(value);
        }

        public void Remove(string argument)
        {
            if (_parameters.ContainsKey(argument))
                _parameters.Remove(argument);
        }

        public bool IsTrue(string argument)
        {
            AssertSingle(argument);

            Collection<string> arg = this[argument];

            return arg != null && arg[0].Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        private void AssertSingle(string argument)
        {
            if (this[argument] != null && this[argument].Count > 1)
                throw new ArgumentException(string.Format("{0} has been specified more than once, expecting single value", argument));
        }

        public string Single(string argument)
        {
            AssertSingle(argument);

            //only return value if its NOT true, there is only a single item for that argument
            //and the argument is defined
            if (this[argument] != null && !IsTrue(argument))
                return this[argument][0];

            return null;
        }

        public bool Exists(string argument) => (this[argument] != null && this[argument].Count > 0);

        private Collection<string> this[string name] => _parameters.ContainsKey(name) ? _parameters[name] : null;
    }
}