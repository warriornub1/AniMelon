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
                script {
                    // Get the published file location (from the container)
                    def publishedFilesPath = "/var/jenkins_home/workspace/learn-jenkins-app/published"
                    
                    // Copy files to the local Downloads folder (running on the Jenkins host)
                    // Ensure the 'Downloads' folder exists on the Jenkins host system
                    sh """
                        mkdir -p /host/publish_folder
                        cp -r ${publishedFilesPath}/* C:/Users/kahyong.chua/Downloads
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
