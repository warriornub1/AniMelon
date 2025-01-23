pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0' // Official .NET 8 SDK image
        }
    }

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
    }

    stages {
        stage('Restore') {
            steps {
                sh 'mkdir -p $DOTNET_CLI_HOME' // Ensure the directory exists
                sh 'dotnet restore'
            }
        }
        
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
        
        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./published'
            }
        }
    }
}
