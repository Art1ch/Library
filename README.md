# How to launch

1. Go to the folder src/Services
2. Start docker-compose file


```
cd src/Services
docker-compose up -d --build
```

# How to test API with swagger?
Insert this into your browser address bar
```https://localhost:8081/swagger/index.html```

# How to authenticate as a Admin?
Send POST-method to this route ```https://localhost:8081/Auth/login```
this body:
```
{
  "email": "admin@gmail.com",
  "password": "admin"
}
```
