using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovChainTextGeneration.Models {
	public class WordsStorage {
		public List<Word> Words { get; set; } = new List<Word>();

		public string GetNextWord(string currentWord) {
			var currentWordObject = Words.FirstOrDefault(w => w.Text == currentWord);
			var nextWords = Words.Where(w => currentWordObject.NextWords.Contains(w));
			int max = nextWords.Sum(w => w.Count);
			int targetIndex = new Random().Next(max);
			int current = 0;
			foreach (var nextWord in nextWords) {
				current += nextWord.Count;
				if (current >= targetIndex)
					return nextWord.Text;
			}
			return "";
		}

		public void AddWord(string word, string nextWord) {
			var wordObject = Words.FirstOrDefault(w => w.Text == word);
			if (wordObject == null) {
				wordObject = new Word{Text = word};
				Words.Add(wordObject);
			}
			else {
				wordObject.Count++;
			}
			if (wordObject.NextWords.FirstOrDefault(w => w.Text == nextWord) == null) {
				var nextWordObject = Words.FirstOrDefault(w => w.Text == nextWord);
				if (nextWordObject == null) {
					nextWordObject = new Word {Text = nextWord};
					Words.Add(nextWordObject);
				}
				wordObject.NextWords.Add(nextWordObject);
			}
		}
		
		public void AddEnd(string end){
			AddWord(end, TextProcessor.END_CONST);
			var wordObject = Words.FirstOrDefault(w => w.Text == TextProcessor.END_CONST);
			if (wordObject == null) {
				wordObject = new Word {Text = TextProcessor.END_CONST};
				Words.Add(wordObject);
			}
			wordObject.Count++;
		}
		
		
	}
}