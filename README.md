Solution Structure (Editor: Visual Studio):
![image](https://github.com/user-attachments/assets/52f7c737-914a-4ef4-9d62-ca34efb00dff)

Projects explanation:
- AddonSystem
    - AddonSample --> Contains Example Addon Assembly projects
    - SanaCommerceAssignment.AddonSystem.AddonBase   --> Base project, contains Assembly Interfaces
    - SanaCommerceAssignment.AddonSystem.ConsoleApp  --> Main Console App
- ConfigurableEditor
    - SanaCommerceAssignment.ConfigurableEditor.API      --> Contains the Backend API Code for ConfigurableEditor Task
    - SanaCommerceAssignment.ConfigurableEditor.Portal   --> Contains the Portal (Both Admin and Client) for Configurable Task
       - To Run this task, you need to start both API and Portal.
       - To use Admin side, Use Admin credentials: (Username: admin@abc.com, Password: Password)
       - To use Client side, Use Client credentials: (Username: client@abc.com, Password: Password)
    - SanaCommerceAssignment.ConfigurableEditor.Shared   --> Contains Share Models
- OrderTransactionAssignment
-   - Script.txt  --> Contains the Task Select script.
- SanaCommerceAssignment.DataServiceTask   --> Data Service Task
- SanaCommerceAssignment.IPRestrictionTask   --> IP Address Restriction Task
