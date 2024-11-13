using Microsoft.AspNetCore.Mvc;
using server.Dtos;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryPackageController : ControllerBase
    {
        
        [HttpPost]
        public double GetSalaryPackaging([FromBody]UserInfo userInfo) {
             float SalaryPackage = 0;
            
            if(userInfo.CompanyType == "CORPORATE")
            {
                if (userInfo.EmploymentType == "CASUAL") return 0;

                if(userInfo.EmploymentType == "PART-TIME")
                {
                    var packagePercentage = (userInfo.HoursWorked / 38) * 100;
                    SalaryPackage = CalculateSalaryPackage((float)packagePercentage, userInfo.Salary);

                    return SalaryPackage;
                }
                if (userInfo.Salary < 100000) 
                {
                    SalaryPackage =CalculateSalaryPackage(1,userInfo.Salary);
                    return SalaryPackage;
                }
                else
                {
                    SalaryPackage = CalculateSalaryPackage((float)0.1, userInfo.Salary);
                    return SalaryPackage;
                }
            }
            if(userInfo.CompanyType == "HOSPITAL")
            {
                float EducationBenifit = 0;

                if(userInfo.EducationLevel =="BACHELOR" || userInfo.EducationLevel == "HIGHER")
                {
                    EducationBenifit = 5000;
                }
                if(userInfo.EmploymentType == "FULL-TIME")
                {
                    var packageBeforeAddition = CalculateSalaryPackage((float)29.5, userInfo.Salary);
                    SalaryPackage = EducationBenifit + packageBeforeAddition + CalculateSalaryPackage((float)1.2,userInfo.Salary);
                    return CheckHospitalSalary(SalaryPackage);
                }

                SalaryPackage=EducationBenifit + CalculateSalaryPackage(20,userInfo.Salary);
                return CheckHospitalSalary(SalaryPackage) ;
            }
            if(userInfo.CompanyType == "PBI")
            {
                float EducationBenifit = 0;

                if (userInfo.EducationLevel == "BACHELOR" || userInfo.EducationLevel == "HIGHER")
                {
                    EducationBenifit = 2000 + CalculateSalaryPackage(1, userInfo.Salary);
                }

                if (userInfo.EmploymentType == "CASUAL")
                {
                    var casualPackage = CalculateSalaryPackage(10,userInfo.Salary);
                    SalaryPackage = casualPackage + EducationBenifit;
                    return CheckPBISalary(SalaryPackage);
                }
              
                if (userInfo.EmploymentType == "PART-TIME")
                {
                    var fullTimeSalary = (userInfo.Salary * 38) / userInfo.HoursWorked;
                    var possibleSalaryPackage = CalculateSalaryPackage(80, (int)fullTimeSalary);
                    SalaryPackage = CalculateSalaryPackage((float)32.55, userInfo.Salary);
                    if(SalaryPackage > possibleSalaryPackage)
                    {
                        return possibleSalaryPackage;
                    }
                    else
                    {
                        return SalaryPackage;
                    }

                }
                SalaryPackage = EducationBenifit + CalculateSalaryPackage((float)32.55, userInfo.Salary);
                return CheckPBISalary(SalaryPackage);
            }
            return 0;
        }
        private float CalculateSalaryPackage(float percentage, int salary)
        {
            if (percentage > 100)
            {
                percentage = 100;
            }
            return (percentage/100 ) * salary;
        }
        private float CheckHospitalSalary(float salaryPackage) 
        {
            if (salaryPackage > 10000)
            {
                return salaryPackage;
            }
            else
            {
                return 10000;
            }
        }
        private float CheckPBISalary(float salaryPackage)
        {
            if (salaryPackage > 50000)
            {
                return 50000;
            }
            else
            {
                return salaryPackage;
            }
        }

    }
}
