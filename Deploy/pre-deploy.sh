#!/bin/sh

timedatectl set-timezone "Asia/Shanghai"

# give a first refresh of all softwares
add-apt-repository universe
add-apt-repository ppa:certbot/certbot
apt update
apt upgrade

# nginx setup
apt install nginx
echo "server {
    listen        80;
    server_name   stage.blog.laobian.me;
    gzip on;
    gzip_types application/manifest+json text/css application/javascript application/rss+xml application/atom+xml image/svg+xml application/xml text/plain text/xml image/png image/jpeg image/gif image/bmp image/x-icon;
    location / {
        proxy_pass         http://localhost:6000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade \$http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host \$host;
        proxy_cache_bypass \$http_upgrade;
        proxy_set_header   X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto \$scheme;
    }
}" | sudo tee /etc/nginx/conf.d/stage.blog.laobian.me.conf >> /dev/null

systemctl enable nginx
systemctl start nginx

# Let's Encrypt setup
apt install software-properties-common
apt install certbot python-certbot-nginx

sudo certbot --nginx -d stage.blog.laobian.me
sudo sed -i "s/listen 443 ssl;/listen 443 ssl http2;/" /etc/nginx/conf.d/stage.blog.laobian.me.conf # a hack to enable HTTP2
systemctl reload nginx
