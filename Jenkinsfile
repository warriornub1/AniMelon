pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v /host_mnt/c/Users/kahyong.chua/Downloads:/mnt/Downloads --user root'
        }
    }

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DEPLOY_PATH = '/mnt/Downloads' // Path inside the container mapped to the host folder
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
                sh 'echo "@@@@@@@@@@@@@@@@@@@@@@@@@@"'
                sh 'ls -l /mnt/Downloads'
                sh 'echo "Path in container: /mnt/Downloads"'
            }
        }
        stage('Deploy to Local Drive') {
            steps {

                echo "Copying files to ${DEPLOY_PATH}"
                
                // Copy files to /mnt/Downloads (mapped to the host directory)
                sh """
                cp -r ./published/* "${DEPLOY_PATH}/"
                """
                
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
