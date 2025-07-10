# Home

Welcome to the Task Management System

Manage and assign tasks with real-time notifications using SignalR.

---

## Step-by-Step Guide

### Step 1: Login as Admin or User (with Logout option)

When you open the system, you'll see a **dropdown menu** where you can select to login as **Admin**, **User1**, **User2**, etc. Your selected user is saved in your browser's localStorage, so the system remembers who you are.

There's also a **Logout** button which removes your login info from localStorage, allowing you to safely switch users or end the session.

  ![image alt](https://raw.githubusercontent.com/Ramaryal33/Task-Management-System/0c20c68cc3c45eb4c4bf0eea8b7d653160cb4df7/1.png)
  ![image alt](https://raw.githubusercontent.com/Ramaryal33/Task-Management-System/14244c345ff257c37ffd21dec21df47b2c85bcaf/2.png)
  ![image alt](https://raw.githubusercontent.com/Ramaryal33/Task-Management-System/49cc61c584a5e996243efd0202a464dc4ec8fd8c/3.png)

### Step 2: Admin creates, edits, updates, and deletes tasks

Only **Admin** can create new tasks, edit existing ones, update details, and delete tasks. On the "Create Task" page, Admin fills in the title, description, deadline date & time, and selects which user to assign the task to. Once submitted, the assigned user receives a real-time notification.

Admin can later edit a task to change its status, update details, or delete it completely. These actions instantly notify the assigned user.

  ![Create task form](https://raw.githubusercontent.com/Ramaryal33/Task-Management-System/0d8d518e6b499616fb8a5db6259f4614c244b430/4.png)
  ![Task list page](https://github.com/Ramaryal33/Task-Management-System/blob/master/5.png?raw=true)
  ![Admin edit task page](https://github.com/Ramaryal33/Task-Management-System/blob/master/6.png?raw=true)

### Step 3: Real-time notification to the assigned user

As soon as a task is created by Admin, the assigned user will instantly see:

- A popup alert showing the task message
- A direct console message like: `You have been assigned a new task by Admin`
- A notification badge showing the number of pending tasks

The system also saves the number of pending tasks in **localStorage** so it remembers and displays the correct badge count even after a page refresh.

This is all powered by SignalR, so everything updates live without refreshing the page.

**To test this properly:**

- Open two browser windows (or two different browsers).
- Login as **Admin** in one, and as **User1** or **User2** in the other.
- Create a task assigned to that user.
- Then watch the user’s window: you’ll see the popup, console message, badge update, and localStorage storing the pending task count.
- Also check that SignalR shows as “Connected” in the console.
  
  ![Notification alert popup](https://github.com/Ramaryal33/Task-Management-System/blob/master/7.png?raw=true)
  ![Console showing message](https://github.com/Ramaryal33/Task-Management-System/blob/master/8.png?raw=true)
  ![SignalR connection status](https://github.com/Ramaryal33/Task-Management-System/blob/master/9.png?raw=true)
  ![LocalStorage pending tasks](https://github.com/Ramaryal33/Task-Management-System/blob/master/10.png?raw=true)
  ![Notification badge updated](https://github.com/Ramaryal33/Task-Management-System/blob/master/11.png?raw=true)
  ![Two browser windows setup](https://github.com/Ramaryal33/Task-Management-System/blob/master/12%20a.png?raw=true)

### Step 4: Admin deletes a task

The Admin can delete tasks. When this happens, the assigned user sees:

- A popup alert about the deleted task
- A console message like: `Task "Task title" was deleted by Admin`

Again, this happens instantly through SignalR.

  ![Delete task confirmation](https://github.com/Ramaryal33/Task-Management-System/blob/master/13.png?raw=true)
  ![Popup alert about deleted task](https://github.com/Ramaryal33/Task-Management-System/blob/master/14.png?raw=true)
  ![Console message about deleted task](https://github.com/Ramaryal33/Task-Management-System/blob/master/15.png?raw=true)
  ![Another deleted task message](https://github.com/Ramaryal33/Task-Management-System/blob/master/16.png?raw=true)

### Step 5: If we Create any Task then it is also save in json file on my project Data/ Task.json
I add rough data seperate to show 
![json file](https://github.com/Ramaryal33/Task-Management-System/blob/57bbb0f3f8f69db5926e539a55583eaf37ec854f/data%202.png)
![json file](https://github.com/Ramaryal33/Task-Management-System/blob/75f86fab3a4c877ca0324a0ae6230d6dd270d67f/data.json.png)


### Step 6: Real-time Updates with SignalR
This application uses SignalR to enable real-time communication between the Admin and Users.

📡 How SignalR Works:
- A SignalR Hub (NotificationHub.cs) is created on the server.
- When a task is created, updated, or deleted by the Admin, a message is sent through the hub.
- The assigned user receives the message instantly via SignalR, without refreshing the page.

![Single R ](https://github.com/Ramaryal33/Task-Management-System/blob/bf4b5a50bcd4c826b9e727b52732ebe82fdb6307/singler%201.png)
![Single R ](https://github.com/Ramaryal33/Task-Management-System/blob/bf4b5a50bcd4c826b9e727b52732ebe82fdb6307/single%20r%202.png)
![Single R ](https://github.com/Ramaryal33/Task-Management-System/blob/bf4b5a50bcd4c826b9e727b52732ebe82fdb6307/single%20r%203.png)

