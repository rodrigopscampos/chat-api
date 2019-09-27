echo Iniciando build da API
dotnet publish -c Release -o out
echo Build finalizado !!!
cls

echo Iniciando build da imagem Docker
docker build . -t chat-api
echo Imagem docker montada !!!
cls

echo Iniciando container "chat-api_1"
docker run --name chat-api_1 -p 5000:5000 chat-api

rem -----------------------------------------------
rem         Principais comandos Docker
rem 
rem docker images -> lista as imagens
rem docker ps -> lista os containers ativos
rem docker ps --all -> lista todos os containers
rem docker attach {container id} -> conecta com o output do container
rem docker top {container id} -> lista os processos do container
rem docker stop {container id} -> para um container
rem docker rm {container id} -> remove um container
rem docker login    -> login no dockerhub
rem docker tag {imagem id} {dockerhub}/{imagem id} -> vincula uma imagem local com uma imagem no dockerhub
rem docker push {dockerhub}/{imagem id} -> atualiza a imagem no dockerhub