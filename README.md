# How to launch

1. Go to the folder src/Services
2. Start docker-compose file


```
cd src/Services
docker-compose up -d --build
```

# How to test API with swagger?
Insert this into your browser address bar
```http://localhost:8080/swagger/index.html```

# How to authenticate as a Admin?
Send POST-method to this route ```http://localhost:8080/Auth/login```
this body:
```
{
  "email": "admin@gmail.com",
  "password": "admin"
}
```
