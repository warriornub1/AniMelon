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
        stage('Test') {
            steps {
                sh 'dotnet test --no-build --verbosity normal'
            }
        }
        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: 'publish/**/*', onlyIfSuccessful: true
        }
    }
}
