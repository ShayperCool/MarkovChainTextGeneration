namespace MarkovChainTextGeneration.Models {
	public interface IWordStorage {
		public string GetNextWord(string currentWord);
		public void AddWord(string word, string nextWord);
	}
}