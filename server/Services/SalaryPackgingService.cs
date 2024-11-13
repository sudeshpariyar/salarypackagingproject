using server.Models;

namespace server.Services
{
    public class SalaryPackgingService
    {
        public SalaryPackagingFinalResult CalculateSalaryPackaging(UserInfo userInfo)
        { 
            if (userInfo.CompanyType == "CORPORATE")
            {
                if (userInfo.EmploymentType == "CASUAL") return new SalaryPackagingFinalResult { TotalSalaryPackagin = 0 };

                double SalaryPackage = CorporateSalaryPackageCalculate(userInfo);
                return new SalaryPackagingFinalResult { TotalSalaryPackagin = SalaryPackage };
             
            }
            if (userInfo.CompanyType == "HOSPITAL")
            {
                double SalaryPackage = HospitalSalaryPackageCalculate(userInfo);
                return new SalaryPackagingFinalResult { TotalSalaryPackagin = SalaryPackage };
              
            }
            if (userInfo.CompanyType == "PBI")
            {
                double SalaryPackage = PBISalaryPackageCalculate(userInfo);
                return new SalaryPackagingFinalResult { TotalSalaryPackagin = SalaryPackage };

            }
            return new SalaryPackagingFinalResult { TotalSalaryPackagin = 0 };
        }
        private double CorporateSalaryPackageCalculate(UserInfo userInfo)
        {
            double packaging = userInfo.Salary < 100000 ? 0.01 * userInfo.Salary : (1000 + 0.001 * (userInfo.Salary - 100000));

            if (userInfo.EmploymentType == "PART-TIME")
            {
                packaging *= userInfo.HoursWorked / 38;
            }
            return packaging;
        }
        private double HospitalSalaryPackageCalculate(UserInfo userInfo)
        {
            double packaging = Math.Max(10000, 0.2 * userInfo.Salary);

            if (userInfo.EducationLevel == "BACHELOR" || userInfo.EducationLevel == "HIGHER")
            {
                packaging += 5000;
            }
            if (userInfo.EmploymentType == "FULL-TIME")
            {
                packaging = packaging * 1.095 + 0.012 * userInfo.Salary;
            }
            return Math.Min(packaging, 30000);

        }
        private double PBISalaryPackageCalculate(UserInfo userInfo)
        {
            double packaging = Math.Min(50000, 0.3255 * userInfo.Salary);
            if (userInfo.EmploymentType == "CASUAL")
            {
                packaging = 0.1 * userInfo.Salary;
            }
            if (userInfo.EducationLevel == "BACHELOR" || userInfo.EducationLevel == "HIGHER")
            {
                packaging += 2000 + 0.01 * userInfo.Salary;
            }
            if (userInfo.EmploymentType == "PART-TIME")
            {
                packaging *= 0.8;
            }
            return packaging;
        }
    }
}

