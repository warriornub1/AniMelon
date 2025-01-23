pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
        }
    }

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DEPLOY_PATH = 'C:\\Users\\kahyong.chua\\Downloads' // Target directory on the local machine
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
        stage('Deploy to Local Drive') {
            steps {
                script {
                    // Use PowerShell to copy files to the local deployment directory
                    powershell """
                    if (!(Test-Path -Path '${DEPLOY_PATH}')) {
                        New-Item -ItemType Directory -Path '${DEPLOY_PATH}'
                    }
                    Copy-Item -Path 'publish\\*' -Destination '${DEPLOY_PATH}' -Recurse -Force
                    """
                }
            }
        }
    }
    post {
        always {
            // Clean up the workspace
            cleanWs()
        }
    }
}
