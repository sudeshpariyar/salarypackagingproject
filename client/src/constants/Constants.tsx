export enum CompanyType {
  HOSPITAL = "HOSPITAL",
  CORPORATE = "CORPORATE",
  PBI = "PBI",
}
export enum EducationLevel {
  BACHELOR = "BACHELOR",
  HIGHER = "HIGHER",
  OTHER = "OTHER",
}

export enum EmploymentType {
  FULLTIME = "FULL-TIME",
  PARTTIME = "PART-TIME",
  CASUAL = "CASUAL",
}

export const companyTypeArray = [
  CompanyType.CORPORATE,
  CompanyType.HOSPITAL,
  CompanyType.PBI,
];
export const edutationTypeArray = [
  EducationLevel.BACHELOR,
  EducationLevel.HIGHER,
  EducationLevel.OTHER,
];
export const employmentTypeArray = [
  EmploymentType.FULLTIME,
  EmploymentType.PARTTIME,
  EmploymentType.CASUAL,
];
