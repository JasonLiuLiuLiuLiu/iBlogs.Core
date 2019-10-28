#!/bin/sh
ASPNETCORE_URLS="http://localhost:6000"

SEND_GRID_API_KEY="..."
ASSET_GITHUB_REPO_API_TOKEN="..."
ASSET_GITHUB_REPO_BRANCH="master"
ASSET_GITHUB_REPO_OWNER="JerryBian"
ASSET_GITHUB_REPO_NAME="laobian-asset-stage"
ASSET_REPO_LOCAL_DIR="/var/www/laobian/stage/asset"
BLOG_ADDRESS="https://stage.blog.laobian.me/"
ASSET_GITHUB_HOOK_SECRET="teststage"

DEPLOY_GITHUB_REPO_API_TOKEN="..."
DEPLOY_GITHUB_REPO_BRANCH="master"
DEPLOY_GITHUB_REPO_OWNER="JerryBian"
DEPLOY_GITHUB_REPO_NAME="laobian-deploy"
DEPLOY_REPO_LOCAL_DIR="/var/www/laobian/stage/deploy"

ADMIN_USER_NAME="test"
ADMIN_PASSWORD="test"

sudo systemctl stop stage.blog.laobian.me
sudo rm -rf "$DEPLOY_REPO_LOCAL_DIR"

git clone "https://$DEPLOY_GITHUB_REPO_API_TOKEN@github.com/$DEPLOY_GITHUB_REPO_OWNER/$DEPLOY_GITHUB_REPO_NAME.git" $DEPLOY_REPO_LOCAL_DIR

echo
echo "Cloned binaries to locally."
echo

DEPLOY_REPO_BLOG="$DEPLOY_REPO_LOCAL_DIR/stage/blog"
DEPLOY_REPO_BLOG_EXE="$DEPLOY_REPO_BLOG/Laobian.Blog"
chmod 764 "$DEPLOY_REPO_BLOG_EXE"

echo "[Unit]
Description=Staging enviroment of Blog
[Service]
WorkingDirectory=$DEPLOY_REPO_BLOG
ExecStart="$DEPLOY_REPO_BLOG_EXE"
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=stage.blog.laobian.me
User=root
Environment=ASPNETCORE_ENVIRONMENT=Staging
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=\"SEND_GRID_API_KEY=$SEND_GRID_API_KEY\"
Environment=\"ASSET_GITHUB_REPO_API_TOKEN=$ASSET_GITHUB_REPO_API_TOKEN\"
Environment=\"ASSET_GITHUB_REPO_BRANCH=$ASSET_GITHUB_REPO_BRANCH\"
Environment=\"ASSET_GITHUB_REPO_OWNER=$ASSET_GITHUB_REPO_OWNER\"
Environment=\"ASSET_GITHUB_REPO_NAME=$ASSET_GITHUB_REPO_NAME\"
Environment=\"ASSET_REPO_LOCAL_DIR=$ASSET_REPO_LOCAL_DIR\"
Environment=\"ASPNETCORE_URLS=$ASPNETCORE_URLS\"
Environment=\"BLOG_ADDRESS=$BLOG_ADDRESS\"
Environment=\"ASSET_GITHUB_HOOK_SECRET=$ASSET_GITHUB_HOOK_SECRET\"
Environment=\"ADMIN_USER_NAME=$ADMIN_USER_NAME\"
Environment=\"ADMIN_PASSWORD=$ADMIN_PASSWORD\"
[Install]
WantedBy=multi-user.target" | sudo tee /etc/systemd/system/stage.blog.laobian.me.service >> /dev/null

sudo systemctl daemon-reload
sudo systemctl enable stage.blog.laobian.me
sudo systemctl start stage.blog.laobian.me

echo
echo "Staging blog is online"
echo
