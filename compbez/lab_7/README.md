1. Install Docker
2. Run it via `docker-compose up`
3. Change the existing code in the project to prevent XSS attack

To run XSS attack, please run the following code in the chat

```<img src='x' width="0" height="0" onerror='alert("XSS")'>```