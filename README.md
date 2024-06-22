# FaceVerifyAttendanceSystem

**Relevance of the Topic**: In modern educational institutions, process automation plays a key role in improving the efficiency and quality of educational services. One of the important tasks is the automation of tracking student attendance in classes. Traditional methods of attendance checking, such as manual marking or access card scanning, have several disadvantages, including high labor intensity, the possibility of errors, and fraud. The use of modern technologies, such as facial recognition, database integration, and web interfaces, can significantly improve this process.
  The proposed system, consisting of a website and a WinForms application, provides automated tracking of student attendance by recognizing faces based on photos uploaded by the students. Interaction between the system components is carried out through a database, which allows efficient storage and processing of attendance information. This approach ensures high accuracy, reduces the likelihood of fraud, and significantly simplifies administrative processes for teachers.
  The development and implementation of such systems are relevant for several reasons. First, it aligns with the general trend of digitalization in education. Second, automating routine tasks allows teachers to devote more time directly to the educational process. Third, modern students expect educational institutions to use the latest technologies, which increases the overall level of satisfaction with the learning experience. Therefore, the creation of such systems is an important step towards improving the quality of educational services and optimizing management processes in educational institutions.

# How to use this program?
1. You need to change the "SqlConnection" string in the appsettings.json file to your own database connection string.
2. You need to perform a migration:
   2.1. add-migration name_Migration, where you need to come up with a name to replace name_Migration
   2.2. update database
3. Obtain Google Credentials on the official Google website.
4. Set up SendGrid on the official website and change the settings in the project. Edit the EmailSenderService file at the following location: FaceVerifyAttendanceSystem.BL.Services.
5. Download this repository -

#Project documentation:
Below are links to the project documentation, including project descriptions, algorithms, database, libraries, and testing results.

**Web application workflow** - [link](https://docs.google.com/document/d/13wmp8NcSBuHHizTNU3T1ifwSVTWXfO4z/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**WinForms application workflow** - [link](https://docs.google.com/document/d/1bF6UCdTdVj1qw4g7k_D6TRRzP5alcmQ1/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Analysis of WinForms application** - [link](https://docs.google.com/document/d/1wybygnRZo26Yl7xqTJP_HmApg1RvjIc6/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Analysis of web application** - [link](https://docs.google.com/document/d/1Qsicj0f4ITgAiKRc1G5chrdqZFS6BbSS/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Class diagram and project architecture description** - [link](https://docs.google.com/document/d/1-ppyww5R3LuO6JbfRKehN4Vv_PixEoaP/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Database structure explanation** - [link](https://docs.google.com/document/d/1FiCwQ8PY6cSCZVViyVr3ITN-SpZVSrAS/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Library descriptions **- [link](https://docs.google.com/document/d/1ZtZ3vuzt8qXI34t9XUFwCw24L5vwENLY/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Use case diagram and key project personnel description** - [link](https://docs.google.com/document/d/1AI1atP_r67G3GP_j6FqJp-p_-PnTylv_/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Testing results** - [link](https://docs.google.com/document/d/1EeI3RYesmDeQI0LmAV2Hu_P7h5twgfI8/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  

**Link to all files on Google Drive** - [link](https://drive.google.com/drive/folders/1fB3H4y2l0RvERrGlw-peuuLWIpQS7l1E?usp=sharing)

#Reminder:
You should remember that there is a current setting requiring your email address to be an educational one to register for the course. To change this, go to the CourseController file in the FaceVerifyAttendanceSystem.BL project in the Controller folder and comment out line 182:  

if (!user.Email.Contains("edu"))  
{  
    TempData["ErrorMessage"] = "Registration is allowed only with educational email addresses.";  
    return RedirectToAction("Error", "Home");  
}  

