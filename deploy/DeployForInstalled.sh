docker pull $(dockerId)/$(imageName):$(Build.BuildNumber)
docker stop $(imageName)
docker rm $(imageName)
docker run -d -p 80:80 --restart=always --name $(imageName) $(dockerId)/$(imageName):$(Build.BuildNumber) DbServer=$(DbServer) DbName=$(DbName) DbUID=$(DbUID) DbPWD=$(DbPWD) BuildNumber=$(Build.BuildNumber)