pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0' // Official .NET 8 SDK image
        }
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
    }
}
