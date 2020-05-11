using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MarkovChainTextGeneration.Models;

namespace MarkovChainTextGeneration {
	public class TextProcessor {

		private const string START_CONST = "[!!!START!!!]";
		private const string END_CONST = "[!!!END!!!]";
		
		private readonly WordsStorage _storage;
		private readonly StorageController _storageController;

		public TextProcessor(WordsStorage storage) {
			_storage = storage;
			_storageController = new StorageController(_storage);
		}
		
		public async Task ProcessTextAsync(string text) {
			text = text.Replace('\n', ' ').Replace('\t', ' ');
			var words = text.Split(' ');
			
			_storageController.AddWord(START_CONST, words[0]);
			
			for (int i = 0; i < words.Length - 1; i++) {
				words[i] = words[i].Replace(" ", "");
				_storageController.AddWord(words[i], words[i + 1]);
			}
			
			_storageController.AddWord(words[^1], END_CONST);
		}

		public async Task<string> GenerateTextAsync() {
			return await Task.Run(GenerateText);
		}

		public string GenerateText() {
			if (_storage.Words.Count == 0)
				return "Words storage is empty!";

			string output = "";
			var nextWord = _storageController.GetWord(START_CONST).SelectRandomNextWord();
			while (nextWord.Text != END_CONST) {
				output += nextWord.Text + " ";
				nextWord = _storageController.GetWord(nextWord.Text).SelectRandomNextWord();
			}

			return output;
		}
		
	}
}