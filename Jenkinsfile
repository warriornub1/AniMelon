pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
        }
    }

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
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
        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./published'
            }
        }
        stage('Print Publish Location') {
            steps {
                // Print out the location of the publish directory
                sh 'echo "Published files are located at: $(pwd)/published"'
            }
        }
    }
}
