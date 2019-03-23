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
  }
 }
stage 'Publish'
 node('') {
     customImage.push()
  customImage.push('latest')
}