environment{
def customImage    
}
stage 'Clone'
 node('') {
  deleteDir()
  checkout scm
 }
stage 'Restore'
 node('') {
  sh 'dotnet restore'
 }
stage 'Build'
 node('') {
  sh 'dotnet build'
  sh 'dotnet publish "whatsapp-api.csproj" -c Release -o ./publish'
 }
stage 'Package'
 node('') {
  docker.withRegistry('', 'credentials-docker'){
  customImage = docker.build("gabrielmuniz95/whatsapp-api:${env.BUILD_ID}")
  echo 'docker tag gabrielmuniz95/whatsapp-api:${env.BUILD_ID} gabrielmuniz95/whatsapp-api:latest'
  }
 }
stage 'Publish'
 node('') {
  customImage.push()
}