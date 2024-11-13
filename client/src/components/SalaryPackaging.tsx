import { ChangeEvent, FormEvent, useState } from "react";
import {
  companyTypeArray,
  edutationTypeArray,
  EmploymentType,
  employmentTypeArray,
} from "../constants/Constants";
import "./SalaryPackaging.css";

interface FormData {
  companyType: string;
  educationLevel: string;
  salary: number;
  employmentType: string;
  hoursWorked: number;
}

const SalaryPackaging = () => {
  const [packaging, setPackaging] = useState();
  const [formData, setFormData] = useState<FormData>({
    companyType: "",
    employmentType: "",
    educationLevel: "",
    salary: 0,
    hoursWorked: 0,
  });

  const handleSelectChange = (e: ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };
  const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };
  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    console.log("FormData: ", formData);
    try {
      const response = await fetch("https://localhost:7229/api/SalaryPackage", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });
      const data = await response.json();
      setPackaging(data);
    } catch (error) {
      console.error("Error:", error);
    }
  };

  return (
    <div className="salary">
      <form className="salary__form" onSubmit={handleSubmit}>
        <span> Type of your Company </span>
        <select
          value={formData.companyType}
          name="companyType"
          onChange={handleSelectChange}
          required
        >
          <option id="0" value="">
            Choose Company Type
          </option>
          {companyTypeArray.map((companyType, index) => (
            <option key={index} value={companyType}>
              {companyType}
            </option>
          ))}
        </select>
        <span>Salary</span>
        <input
          type="number"
          name="salary"
          onChange={handleInputChange}
          value={formData.salary}
          min={1}
        />
        <span>Education Level</span>
        <select
          value={formData.educationLevel}
          onChange={handleSelectChange}
          name="educationLevel"
          required
        >
          <option id="0" value="">
            Choose Education Level
          </option>
          {edutationTypeArray.map((educationLevel, index) => (
            <option key={index} value={educationLevel}>
              {educationLevel}
            </option>
          ))}
        </select>

        <span>Employment Type</span>
        <select
          value={formData.employmentType}
          onChange={handleSelectChange}
          name="employmentType"
          required
        >
          <option id="0" value="">
            Choose Employment Type
          </option>
          {employmentTypeArray.map((employmentType, index) => (
            <option key={index} value={employmentType}>
              {employmentType}
            </option>
          ))}
        </select>
        {formData.employmentType === EmploymentType.PARTTIME && (
          <>
            <label>Hours Worked</label>
            <input
              placeholder="Hours Worked"
              onChange={handleInputChange}
              value={formData.hoursWorked}
              type="number"
              name="hoursWorked"
              min={1}
            />
          </>
        )}
        <button type="submit">Submit</button>
      </form>
      <div>{packaging && <h4>Your salary packaging is ${packaging}</h4>}</div>
    </div>
  );
};

export default SalaryPackaging;
