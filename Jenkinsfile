pipeline {
    agent any

    stages {
        stage('Build') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0'
                    reuseNode true
                }
            }
            steps {
                sh '''
                    stage('Restore Dependencies') {
                        steps {
                            sh 'dotnet restore'
                        }
                    }
            
                    stage('Build Project') {
                        steps {
                            sh 'dotnet build --configuration Release'
                        }
                    }
                '''
            }
        }
    }
}
