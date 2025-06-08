# MyAssistant

Web application project by Vladislav Grublyak. For cooperation - grublyakvlad@yandex.ru

The goal of this project is to provide users with functionality that allows them to keep personal training diaries, as well as track and record their progress.

The advantage of implementing it as a web application is that the user has access to the functionality at any time and from any device with Internet access.

## Project architecture.

This project has a micro-service architecture.

The project consists of the following containers:

- authservice - our own development based on ASP.net application. Back-end. Implements API for user authentication and authorization.

- sportsservice - our own development based on ASP.net application. Back-end. Implements API for working with the main functionality of the application.

- notificationservice - our own development based on ASP.net application. Back-end. Implements functionality for notifying users. (Under development)

- webservice - our own development based on Node.JS + React application. Front-end. Implements a web client for interacting with the user.

- nginx-1.26.2-alpine - image from Docker Hub. Proxy-server + API-Getaway. Processes all incoming https requests to the server and redirects to the required service.

- rabbitmq-4.0.4-alpine - image from Docker Hub. Message broker for interaction of Back-end services.

- postgres.17.2 - image from Docker Hub. DBMS for Back-end services. Moreover, each service has its own DB.

- pgadmin4.8.13.0 - image from Docker Hub. Graphical DBMS client.

Portainer is used to administer containers.

  
You can see the implementation of this project at the link https://my-assistant-dev.ru
  
## Implementation details.

When requesting on the server's https port, it is processed by Nginx (example config => MyAssistant/proxy/nginx.conf). If the request is sent to /api/auth/ or /api/sports/ - the request is redirected to the corresponding Back-end container. In other cases, the request is sent to the Front-end container.

The Front-end checks for the presence of an authorization cookie and forcibly redirects to the page https://my-assistant-dev.ru/login if the user is not authorized. In other cases, the user has access to the full functionality of the site. When interacting with the UI, the web application also sends requests to the Back-end containers.

When requesting authorization to log in to the system, the service checks the received Email and password hash and, if it matches the data in the database, provides the user with the necessary Cookie. This Cookie is Secure HttpOnly Cookie. The Front-end sends it with all user requests (if any) and does not have access to it.

When processing a request, the Back-end service analyzes the authorization cookie and, based on this data, makes a decision on the user's access to the requested resource. 

- For a description of the API authservice - http://194.87.201.90:8081/swagger/index.html

- For a description of the API sportsservice: - http://194.87.201.90:8082/swagger/index.html (Description only. Without the received authorization cookie, most methods return 401. It is necessary to authorize the user from one client (for example, my-assistant-dev.ru or postman) and then gain access to the functionality on behalf of this user. In this case, it is necessary to form a request by domain name or directly to the https port.)  

## Comment on webservice.

This service is implemented in test mode to describe interaction with Back-end services. Further redesign and addition of input field validation are planned. However, during the implementation of the project, a rule was established: tasks on the Back-end side are solved first. I deal with the Front-end side last.

If you want to develop your own client for my service, please contact me by email at grublyakvlad@yandex.ru

Further development plans:

1. Add functionality for completing training sessions in the Front-end;

2. Publish a notification container with functionality for sending notifications to email.