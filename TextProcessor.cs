using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MarkovChainTextGeneration.Models;

namespace MarkovChainTextGeneration {
	public class TextProcessor {

		public const string START_CONST = "[!!!START!!!]";
		public const string END_CONST = "[!!!END!!!]";
		private readonly int _maxWords;
		
		private readonly WordsStorage _storage;

		public TextProcessor(WordsStorage storage, int maxWords = 20) {
			_storage = storage;
			_maxWords = maxWords;
		}
		
		public async Task ProcessTextAsync(string text) {
			await Task.Run(() => ProcessText(text));
		}

		public void ProcessText(string text) {
			text = text.Replace('\n', ' ').Replace('\t', ' ');
			var words = text.Split(' ');
			
			_storage.AddWord(START_CONST, words[0]);
			
			for (int i = 0; i < words.Length - 1; i++) {
				words[i] = words[i].Replace(" ", "");
				_storage.AddWord(words[i], words[i + 1]);
			}
			
			_storage.AddEnd(words[^1]);
		}
		
		public async Task<string> GenerateTextAsync() {
			return await Task.Run(GenerateText);
		}

		public string GenerateText() {
			if (_storage.Words.Count == 0)
				return "Words storage is empty!";
			string output = "";
			var nextWord = _storage.GetNextWord(START_CONST);
			int count = 0;
			while (nextWord != END_CONST && count < _maxWords) {
				output += nextWord + " ";
				count++;
				nextWord = _storage.GetNextWord(nextWord);
			}

			return output;
		}
		
	}
}