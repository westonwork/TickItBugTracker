## TickIt Bug Tracker

Software bug tracker, built using C#, ASP.NET, & MySQL.

#### Goals: 

I designed this app with 2 goals in mind: 

  1. Minimalist Design Style

     Since tackling bugs can be stressful for the developer, I wanted to incorporate a minimalist design to the app with a heavy use of whites and clean simple lines. The navigation page is simple to allow the developer to leave it open on a screen and not feel cluttered or chaotic, but invoke a sense of calm and order. 
    
  2. User Features
  
     The app differentiates who can add or remove a specific ticket from the tracker by validating who is logged in and who initially created the ticket. It also provides a separate table of just the bugs the logged in developer has added.


  Why a Bug Tracker? In my research prior to wireframing this app, I found that many bug trackers currently on the market have a lot of features that a newer developer may not need. This bug tracker currently focuses on just adding and completing tickets.


#### Implementation: 

  - Utilized Bootstrap and custom CSS to create an intuitive and minimalist user-interface and landing page.
  - Established database using MYSQL and made back-end calls using LINQ to query the database and display tickets associated with all users, currently logged-in user, and assign new users to unassigned tickets. 
  - Implemented validations to verify ticket information can only be edited or deleted by the user who created the ticket.


#### Biggest Hurdle: 

  My initial goal for the app included adding a notepad feature for the developer to leave notes for themselves beyond only within a ticket. This feature is still being researched. 


#### Timeline & Role: 

  - One week to come up with idea, wireframe it, and create MVP to complete the C#/.NET Stack of my coursework at Coding Dojo full-time software development program. 
  - This was a solo, personal project, where I was the Full-Stack Developer. 
