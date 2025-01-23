pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v /c/Users/kahyong.chua/Downloads:/mnt/Downloads --user root'
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
                sh "echo Deploying to : ${DEPLOY_PATH}"
                sh 'echo "Host Downloads directory: /c/Users/kahyong.chua/Downloads"' // Added step to verify host path
            }
        }
        stage('Deploy to Local Drive') {
            steps {
                // Check contents of /mnt/Downloads before copying
                sh 'echo "Listing contents of /mnt/Downloads before copying"'
                sh 'ls -la /mnt/Downloads'
                
                // Check if files exist in the published directory
                sh 'echo "Listing contents of ./published before copying"'
                sh 'ls -la ./published'
                
                echo "Copying files to ${DEPLOY_PATH}"
                
                // Copy files to /mnt/Downloads (mapped to the host directory)
                sh """
                cp -r ./published/* "${DEPLOY_PATH}/"
                """
                
                // Check contents of /mnt/Downloads after copying
                sh 'echo "Listing contents of /mnt/Downloads after copying"'
                sh 'ls -la /mnt/Downloads'
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
