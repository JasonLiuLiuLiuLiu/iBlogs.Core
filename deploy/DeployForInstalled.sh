docker pull liuzhenyulive/iblogs
docker stop iblogs
docker rm iblogs
docker run -d -p 80:80 --restart=always --name iblogs liuzhenyulive/iblogs DbServer=${DbServer} DbName=${DbName} DbUID=${DbUID} DbPWD=${DbPWD}