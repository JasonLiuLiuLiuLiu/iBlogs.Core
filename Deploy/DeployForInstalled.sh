docker stop $(imageName)
docker rm $(imageName)
docker rm -f $(docker ps -a |  grep "$(imageName)*"  | awk '{print $1}')
docker pull $(dockerId)/$(imageName):$(Build.BuildNumber)
cd ~
mkdir iBlogsLog
docker run -d -p 5000:80 --restart=always --name $(imageName) $(dockerId)/$(imageName):$(Build.BuildNumber) GitUrl=$(GitUrl) GitUid=$(GitUid) GitPwd=$(GitPWD) -v /iBlogsLog:/log