using System.Text.Json;

namespace JSONExample
{
    public class Entry
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }

        private string _aField = "Hello";
        private int _intField = 30;

        public Entry(string name, int age, string occupation) {
            Name = name;
            Age = age;
            Occupation = occupation;
        }


        public async Task WritetoJson(string filename) {
            string json = JsonSerializer.Serialize(this);
            using (FileStream outputStream = File.Create(filename)) {
                using (StreamWriter writer = new StreamWriter(outputStream)) {
                    await writer.WriteAsync(json);
                }
            }
        }
    }
}
