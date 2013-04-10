namespace Web.Models
{
    public class Person
    {
        public string Name { get; private set; }
        public string Company { get; private set; }
        public int Age { get; private set; }

        public static Person Create(string name, int age, string company)
        {
            return new Person()
                {
                    Age = age,
                    Company = company,
                    Name = name
                };
        }
    }
}