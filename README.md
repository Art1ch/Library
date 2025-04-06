# How to launch

1. Go to the folder src/Services
2. Start docker-compose file


```
cd src/Services
docker-compose up -d --build
```

# How to authenticate as a Admin?
Send to the route ```https://localhost:51497/Auth/login```
this body:
```
{
  "email": "admin@gmail.com",
  "password": "admin"
}
```
