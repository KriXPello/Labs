const express = require('express');
const app = express();
const http = require('http');
const server = http.createServer(app);
const fs = require('fs');
const path = require('path');
const { Server } = require("socket.io");

const PORT = 3000;
const io = new Server(server);

app.use(express.static(__dirname + '/../public'));

const pathToCryptoJs = path.join(process.cwd(), 'node_modules', 'crypto-js', 'crypto-js.js');
const cryptoJs = fs.readFileSync(pathToCryptoJs, 'utf8');

app.get('/crypto-js.js', (req, res) => {
    res.send(cryptoJs);
});

io.on('connection', (socket) => {
    socket.on('new message', (msg) => {
        console.log('new message:', msg);
        io.emit('new message', msg);
    });
});

server.listen(PORT, () => {
    console.log(`Server is running on *:${PORT}`);
});
