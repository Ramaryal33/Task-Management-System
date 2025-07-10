# Home

Welcome to the Task Management System

Manage and assign tasks with real-time notifications using SignalR.

---

## Step-by-Step Guide

### Step 1: Login as Admin or User (with Logout option)

When you open the system, you'll see a **dropdown menu** where you can select to login as **Admin**, **User1**, **User2**, etc. Your selected user is saved in your browser's localStorage, so the system remembers who you are.

There's also a **Logout** button which removes your login info from localStorage, allowing you to safely switch users or end the session.

<div style="display:flex;gap:10px;flex-wrap:wrap">
  ![image alt](https://raw.githubusercontent.com/Ramaryal33/Task-Management-System/51bf5ee2439d8011ed4dc9d7255836d598aa29c0/1.png)
  ![Login dropdown and logout button](images/2.png)
</div>

![Extra login screenshot](images/3.png)

---

### Step 2: Admin creates, edits, updates, and deletes tasks

Only **Admin** can create new tasks, edit existing ones, update details, and delete tasks. On the "Create Task" page, Admin fills in the title, description, deadline date & time, and selects which user to assign the task to. Once submitted, the assigned user receives a real-time notification.

Admin can later edit a task to change its status, update details, or delete it completely. These actions instantly notify the assigned user.

<div style="display:flex;gap:10px;flex-wrap:wrap">
  ![Create task form](images/4.png)
  ![Task list page](images/5.png)
  ![Admin edit task page](images/6.png)
</div>

---

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

<div style="display:flex;gap:10px;flex-wrap:wrap">
  ![Notification alert popup](images/7.png)
  ![Console showing message](images/8.png)
  ![SignalR connection status](images/9.png)
</div>

<div style="display:flex;gap:10px;flex-wrap:wrap">
  ![LocalStorage pending tasks](images/10.png)
  ![Notification badge updated](images/11.png)
  ![Two browser windows setup](images/12 a.png)
</div>

---

### Step 4: Admin deletes a task

The Admin can delete tasks. When this happens, the assigned user sees:

- A popup alert about the deleted task
- A console message like: `Task "Task title" was deleted by Admin`

Again, this happens instantly through SignalR.

<div style="display:flex;gap:10px;flex-wrap:wrap">
  ![Delete task confirmation](images/13.png)
  ![Popup alert about deleted task](images/14.png)
  ![Console message about deleted task](images/15.png)
  ![Another deleted task message](images/16.png)
</div>
