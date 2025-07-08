namespace DataGridSampleProject
{
    public class Employee
    {
        public int Id;
        public string Name;
        public string Designation;
        public string EmailId;
        public string Reporter;
        public string Reportee;
        public string ProductLineResponsibility;
        public int WorkExperience; 

        public Employee (int id, string name, string designation, string emailId,
        string reporter, string reportee, string productLineResponsibility, int workExperience)
        {
            this.Id = id;
            this.Name = name;
            this.Designation = designation;
            this.EmailId = emailId;
            this.Reporter = reporter;
            this.Reportee = reportee;
            this.ProductLineResponsibility = productLineResponsibility;
            this.WorkExperience = workExperience;
        }
    }
}