namespace DataGridSampleProject
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string EmailId { get; set; }
        public string Reporter { get; set; }
        public string Reportee { get; set; }
        public string ProductLineResponsibility { get; set; }
        public int WorkExperience { get; set; }

        /// <summary>
        /// Parameterless Construction for XML serialization 
        /// </summary>
        public Employee() { }

        public Employee(int id, string name, string designation, string emailId,
        string reporter, string reportee, string productLineResponsibility, int workExperience)
        {
            Id = id;
            Name = name;
            Designation = designation;
            EmailId = emailId;
            Reporter = reporter;
            Reportee = reportee;
            ProductLineResponsibility = productLineResponsibility;
            WorkExperience = workExperience;
        }

        public override string ToString()
        {
            return string.Concat
            (
                Id.ToString(), 
                "__",
                Name,
                "__",
                Designation,
                "__",
                EmailId,
                "__",
                Reporter,
                "__",
                Reportee,
                "__",
                ProductLineResponsibility,
                "__",
                WorkExperience
            ); 
        }
    }
}