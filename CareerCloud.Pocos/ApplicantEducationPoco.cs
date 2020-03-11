// Assignment 1 for C#
//Name: Bhavesh Solanki
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    //Table [JOB_PORTAL_DB].[DBO].[APPLICANT_EDUCATIONS]
    [Table("Applicant_Educations")]
    public class ApplicantEducationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string Major { get; set; }
        [Column("Certificate_Diploma")]
        public string CertificateDiploma { get; set; }
        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }
        [Column("Completion_Date")]
        public DateTime? CompletionDate { get; set; }
        [Column("Completion_Percent")]
        public Byte? CompletionPercent { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }

        public virtual ApplicantProfilePoco ApplicantProfile_ApplicantEducation {get;set;}
    }

    //Table [JOB_PORTAL_DB].[DBO].[APPLICANT_JOB_APPLICATONS]
    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public Guid Job { get; set; }
        [Column("Application_Date")]
        public DateTime ApplicationDate { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile_ApplicantJobApplication { get; set; }
        public virtual CompanyJobPoco CompanyJob_ApplicantJobApplication { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Applicant_Profiles]
    [Table("Applicant_Profiles")]
    public class ApplicantProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Column("Current_Salary")]
        public decimal? CurrentSalary { get; set; }
        [Column("Current_Rate")]
        public decimal? CurrentRate { get; set; }
        public String Currency { get; set; }
        [Column("Country_Code")]
        public String Country { get; set; }
        [Column("State_Province_Code")]
        public String Province { get; set; }
        [Column("Street_Address")]
        public String Street { get; set; }
        [Column("City_Town")]
        public String City { get; set; }
        [Column("Zip_Postal_Code")]
        public String PostalCode { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }

        public virtual SecurityLoginPoco SecurityLogin_ApplicantProfile { get; set; }

        public virtual SystemCountryCodePoco SystemCountryCode_ApplicantProfile { get; set; }
        public virtual ICollection<ApplicantEducationPoco> ApplicationEducations_ApplicantProfile {get;set;}
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications_ApplicantProfile { get; set; }
        public virtual ICollection<ApplicantResumePoco> ApplicantResumes_ApplicantProfile { get; set; }
        public virtual ICollection<ApplicantSkillPoco> ApplicantSkills_ApplicantProfile { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistorys_ApplicantProfile { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Applicant_Resumes]
    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public String Resume { get; set; }
        [Column("Last_Updated")]
        public DateTime? LastUpdated { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile_ApplicantResume { get; set; }
    }

    //Table  [JOB_PORTAL_DB].[DBO].[Applicant_Skills]
    [Table("Applicant_Skills")]
    public class ApplicantSkillPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public String Skill { get; set; }
        [Column("Skill_Level")]
        public String SkillLevel { get; set; }
        [Column("Start_Month")]
        public Byte StartMonth { get; set; }
        [Column("Start_Year")]
        public int StartYear { get; set; }
        [Column("End_Month")]
        public byte EndMonth { get; set; }
        [Column("End_Year")]
        public int EndYear { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile_ApplicantSkill { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Applicant_Work_History]
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        [Column("Company_Name")]
        public String CompanyName { get; set; }
        [Column("Country_Code")]
        public String CountryCode { get; set; }
        public String Location { get; set; }
        [Column("Job_Title")]
        public String JobTitle { get; set; }
        [Column("Job_Description")]
        public String JobDescription { get; set; }
        [Column("Start_Month")]
        public short StartMonth { get; set; }
        [Column("Start_Year")]
        public int StartYear { get; set; }
        [Column("End_Month")]
        public short EndMonth { get; set; }
        [Column("End_Year")]
        public int EndYear { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile_ApplicantWorkHistory { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode_ApplicantWorkHistory { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Descriptions]
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        public String LanguageId { get; set; }
        [Column("Company_Name")]
        public String CompanyName { get; set; }
        [Column("Company_Description")]
        public String CompanyDescription { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual CompanyProfilePoco CompanyProfile_CompanyDescription { get; set; }
        public virtual SystemLanguageCodePoco SystemLanguageCode_CompanyDescription { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Job_Educations]
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public String Major { get; set; }
        public short Importance { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual CompanyJobPoco CompanyJob_CompanyJobEducation { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Job_Skills]
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public String Skill { get; set; }
        [Column("Skill_Level")]
        public String SkillLevel { get; set; }
        public int Importance { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual CompanyJobPoco CompanyJob_CompanyJobSkill { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Jobs]
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual CompanyProfilePoco CompanyProfile_CompanyJob { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplication_CompanyJobs { get; set; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations_CompanyJob { get; set; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills_CompanyJob { get; set; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescriptions_CompanyJob { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Jobs_Descriptions]
    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        [Column("Job_Name")]
        public String JobName { get; set; }
        [Column("Job_Descriptions")]
        public String JobDescriptions { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual CompanyJobPoco CompanyJob_CompanyJobDescription { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Locations]
    [Table("Company_Locations")]
    public class CompanyLocationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("Country_Code")]
        public String CountryCode { get; set; }
        [Column("State_Province_Code")]
        public String Province { get; set; }
        [Column("Street_Address")]
        public String Street { get; set; }
        [Column("City_Town")]
        public String City { get; set; }
        [Column("Zip_Postal_Code")]
        public String PostalCode { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual CompanyProfilePoco CompanyProfile_CompanyLocation { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Company_Profiles]
    [Table("Company_Profiles")]
    public class CompanyProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; }
        [Column("Company_Website")]
        public String CompanyWebsite { get; set; }
        [Column("Contact_Phone")]
        public String ContactPhone { get; set; }
        [Column("Contact_Name")]
        public String ContactName { get; set; }
        [Column("Company_Logo")]
        public Byte[] CompanyLogo { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions_CompanyProfile { get; set; }
        public virtual ICollection<CompanyJobPoco> CompanyJobs_CompanyProfile { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyProfiles_CompanyLocation { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Security_Logins]
    [Table("Security_Logins")]
    public class SecurityLoginPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        [Column("Created_Date")]
        public DateTime Created { get; set; }
        [Column("Password_Update_Date")]
        public DateTime? PasswordUpdate { get; set; }
        [Column("Agreement_Accepted_Date")]
        public DateTime? AgreementAccepted { get; set; }
        [Column("Is_Locked")]
        public Boolean IsLocked { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Email_Address")]
        public String EmailAddress { get; set; }
        [Column("Phone_Number")]
        public String PhoneNumber { get; set; }
        [Column("Full_Name")]
        public String FullName { get; set; }
        [Column("Force_Change_Password")]
        public Boolean ForceChangePassword { get; set; }
        [Column("Prefferred_Language")]
        public String PrefferredLanguage { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual ICollection<ApplicantProfilePoco>  ApplicantProfiles_SecurityLogin { get; set; }
        public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLogs_SecurityLogin { get; set; }
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles_SecurityLogin { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Security_Logins_Log]
    [Table("Security_Logins_Log")]
    public class SecurityLoginsLogPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Column("Source_IP")]
        public String SourceIP { get; set; }
        [Column("Logon_Date")]
        public DateTime LogonDate { get; set; }
        [Column("Is_Succesful")]
        public Boolean IsSuccesful { get; set; }
        public virtual SecurityLoginPoco SecurityLogin_SecurityLoginsLog { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Security_Logins_Roles]
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public Guid Role { get; set; }
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
        public virtual SecurityLoginPoco SecurityLogin_SecurityLoginsRole { get; set; }
        public virtual SecurityRolePoco SecurityRole_SecurityLoginsRole { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[Security_Roles]
    [Table("Security_Roles")]
    public class SecurityRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public String Role { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles_SecurityRole { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[System_Security_Codes]
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco
    {
        [Key]
        public String Code { get; set; }
        public String Name { get; set; }
        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles_SystemCountryCode { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistorys_SystemCountryCode { get; set; }
    }

    //Table [JOB_PORTAL_DB].[DBO].[System_Language_Codes]
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco
    {
        [Key]
        public String LanguageID { get; set; }
        public String Name { get; set; }
        [Column("Native_Name")]
        public String NativeName { get; set; }
        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions_SystemLanguageCode { get; set; }
    }
}
