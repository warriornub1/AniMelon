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
                sh 'echo "Published files are located at: $(pwd)/published"'
            }
        }
        stage('Copy to Local') {
            steps {
                // Copy files from the container to the local system using docker cp
                script {
                    def containerId = sh(script: "docker ps -q -f ancestor=mcr.microsoft.com/dotnet/sdk:8.0", returnStdout: true).trim()
                    if (containerId) {
                        // Ensure the directory exists in your Downloads
                        sh "docker cp ${containerId}:/workspace/published/. C:/Users/kahyong.chua/Downloads"
                    }
                }
            }
        }
    }
}
