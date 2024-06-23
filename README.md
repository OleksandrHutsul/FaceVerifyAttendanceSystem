# **FaceVerifyAttendanceSystem**

**Relevance of the Topic**: In modern educational institutions, process automation plays a key role in improving the efficiency and quality of educational services. One of the important tasks is the automation of tracking student attendance in classes. Traditional methods of attendance checking, such as manual marking or access card scanning, have several disadvantages, including high labor intensity, the possibility of errors, and fraud. The use of modern technologies, such as facial recognition, database integration, and web interfaces, can significantly improve this process.
  The proposed system, consisting of a website and a WinForms application, provides automated tracking of student attendance by recognizing faces based on photos uploaded by the students. Interaction between the system components is carried out through a database, which allows efficient storage and processing of attendance information. This approach ensures high accuracy, reduces the likelihood of fraud, and significantly simplifies administrative processes for teachers.
  The development and implementation of such systems are relevant for several reasons. First, it aligns with the general trend of digitalization in education. Second, automating routine tasks allows teachers to devote more time directly to the educational process. Third, modern students expect educational institutions to use the latest technologies, which increases the overall level of satisfaction with the learning experience. Therefore, the creation of such systems is an important step towards improving the quality of educational services and optimizing management processes in educational institutions.

# **How to use web program?**
1. You need to change the "SqlConnection" string in the appsettings.json file to your own database connection string.
2. You need to perform a migration:
   2.1. add-migration name_Migration, where you need to come up with a name to replace name_Migration
   2.2. update database
3. Obtain Google Credentials on the official Google website.
4. Set up SendGrid on the official website and change the settings in the project. Edit the EmailSenderService file at the following location: FaceVerifyAttendanceSystem.BL.Services.
5. Create an appropriate folder on Google Drive and replace the ID in the code with your own storage. This can be done by changing a line of code in the file FaceVerifyAttendanceSystem\FaceVerifyAttendanceSystem.UI\Areas\Identity\Pages\Account\Manage\Index.cshtml.cs. Change this line (16):  
*private const string folderId = "1XyKmDPzNCJVIUze8wNATXKakwOPni2n4";*
Also, the obtained Credential file needs to be added to the folder - FaceVerifyAttendanceSystem\FaceVerifyAttendanceSystem.UI\bin\Debug\net8.0. If there is already a credential.json file, delete my file and replace it with your own.
7. Download this archive - [link](https://drive.google.com/file/d/1o9TaJCIvYrZ_YTnKnfXQLd-zOoPXYo6K/view?usp=sharing)  
The explanation for why it is an archive and not a link to a GitHub repository is that I spent several days trying to figure out why I couldn't upload this particular build there, and I still couldn't determine the problem. This issue is a first for me and has only occurred with this project. So, if anyone finds a solution, I would be very happy to fix it and replace the archive link with a link to the GitHub repository. Also, here you can see a screenshot that confirms my words - [link](https://drive.google.com/file/d/1GFJjBq82OjMdwCTaAtKyXwfBdOyNrOMe/view?usp=sharing)

# **Project documentation:**
Below are links to the project documentation, including project descriptions, algorithms, database, libraries, and testing results.

**Web application workflow** - [link](https://docs.google.com/document/d/13wmp8NcSBuHHizTNU3T1ifwSVTWXfO4z/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**WinForms application workflow** - [link](https://docs.google.com/document/d/1bF6UCdTdVj1qw4g7k_D6TRRzP5alcmQ1/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Analysis of WinForms application** - [link](https://docs.google.com/document/d/1wybygnRZo26Yl7xqTJP_HmApg1RvjIc6/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Analysis of web application** - [link](https://docs.google.com/document/d/1Qsicj0f4ITgAiKRc1G5chrdqZFS6BbSS/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Class diagram and project architecture description** - [link](https://docs.google.com/document/d/1-ppyww5R3LuO6JbfRKehN4Vv_PixEoaP/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Database structure explanation** - [link](https://docs.google.com/document/d/1FiCwQ8PY6cSCZVViyVr3ITN-SpZVSrAS/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Library descriptions** - [link](https://docs.google.com/document/d/1ZtZ3vuzt8qXI34t9XUFwCw24L5vwENLY/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Use case diagram and key project personnel description** - [link](https://docs.google.com/document/d/1AI1atP_r67G3GP_j6FqJp-p_-PnTylv_/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  
**Testing results** - [link](https://docs.google.com/document/d/1EeI3RYesmDeQI0LmAV2Hu_P7h5twgfI8/edit?usp=sharing&ouid=114473835927882897867&rtpof=true&sd=true)  

**Link to all files on Google Drive** - [link](https://drive.google.com/drive/folders/1fB3H4y2l0RvERrGlw-peuuLWIpQS7l1E?usp=sharing)

# **Reminder:**
You should remember that there is a current setting requiring your email address to be an educational one to register for the course. To change this, go to the CourseController file in the FaceVerifyAttendanceSystem.BL project in the Controller folder and comment out line 182:  

*if (!user.Email.Contains("edu"))  
{  
    TempData["ErrorMessage"] = "Registration is allowed only with educational email addresses.";  
    return RedirectToAction("Error", "Home");  
}*  

# **How to use WinForms program?**
If, for some reason, you missed downloading the archive, here is the link to it - [link](https://drive.google.com/file/d/1o9TaJCIvYrZ_YTnKnfXQLd-zOoPXYo6K/view?usp=sharing). After you unpack the archive, you need to follow a few steps to make everything work correctly:
1. Go to the Google Development site and get a Google OAuth client;  
2. After downloading the client, go to the folder FaceVerifyAttendanceSystemAppUI\FaceVerifyAttendanceSystemAppUI\bin\x64\Debug\net8.0-windows - delete the credentials.json file from there and add your own credentials file.  
3. Delete the FaceRegonationDotNet library and reinstall it. This is to ensure the correct configuration of the files.  
4. Change the build architecture, the FaceRegonationDotNet library only works with x64.  
5. Replace the link in the DataBase file with your own database link.

# **Database Fields Configuration**
Since the code does not include automatic field configuration, here is what you need to manually add to the database:

1. You need to fill in the AspNetRoles table exactly as shown in this picture:
![image](https://github.com/OleksandrHutsul/FaceVerifyAttendanceSystem/assets/111017111/6ccd4c06-af19-4b69-ac7a-4d9a99a9d22a)
  
2. If you need an administrator, after successful registration, you need to go to the AspNetUsers table to check the ID of the registered user, then go to the AspNetRoles table to check the ID of the admin role, then go to the AspNetUserRoles table and fill it in manually.
