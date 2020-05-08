systemctl stop iblogs
rm -rf /iblogs/www
mkdir /iblogs/www
unzip $(System.DefaultWorkingDirectory)/_liuzhenyulive.iBlogs.Core/drop/iBlogs.Site.Web.zip -d /iblogs/www
cat > /iblogs/.setting<<-EOF
GitUrl=$(GitUrl)
GitUid=$(GitUid)
GitPwd=$(GitPwd)
BuildNumber=$(Build.BuildNumber)
JwtKey=$(JwtKey)
EOF
systemctl start iblogs