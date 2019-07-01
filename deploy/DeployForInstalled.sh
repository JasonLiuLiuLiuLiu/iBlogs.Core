 #!/bin/bash 
 for i; do 
    echo $i 
 done
docker pull $DOCKERID/$IMAGENAME:$BUILD_BUILDNUMBER
docker stop $IMAGENAME
docker rm $IMAGENAME
docker run -d -p 80:80 --restart=always --name $IMAGENAME $DOCKERID/$IMAGENAME:$BUILD_BUILDNUMBER DbServer=$DBSERVER DbName=$DBNAME DbUID=$DBUID DbPWD=$DBPWD BuildNumber=$BUILD_BUILDNUMBER