using System.Collections.Generic;
using MarkovChainTextGeneration.Models;

namespace MarkovChainTextGeneration {
	public class StorageController {

		private readonly WordsStorage _wordsStorage;

		public StorageController(WordsStorage wordsStorage) {
			_wordsStorage = wordsStorage;
		}
		

		public void AddWord(string word, string nextWord) {
			if (_wordsStorage.Words.ContainsKey(word)) {
				AddNextWord(word, nextWord);
			}
			else {
				AddNewWord(word, nextWord);
			}
		}
		
		public Word GetWord(string word) {
			return _wordsStorage.Words[word];
		}

		private void AddNextWord(string word, string nextWord) {
			var wordObject = _wordsStorage.Words[word];
			if (wordObject.NextWords.ContainsKey(nextWord)) {
				wordObject.NextWords[nextWord].Count++;
			}
			else {
				wordObject.NextWords.Add(nextWord, new Word {
					Count = 1,
					Text = nextWord,
				});
			}
		}

		private void AddNewWord(string word, string nextWord) {
			var nextWordObject = new Word {
				Count = 1,
				Text = nextWord,
			};
			var wordObject = new Word {
				Count = 1,
				Text = word
			};
			wordObject.NextWords.Add(nextWord, nextWordObject);
			_wordsStorage.Words.Add(word, wordObject);
		}
	}
}