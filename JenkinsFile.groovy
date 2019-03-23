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
  customImage = docker.build("rodrigopscampos/whatsapp-api:${env.BUILD_ID}")
  sh 'docker tag rodrigopscampos/whatsapp-api:${env.BUILD_ID} "rodrigopscampos/whatsapp-api:latest'
  }
 }
stage 'Publish'
 node('') {
  customImage.push()
}