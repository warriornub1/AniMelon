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
                sh """
                # Publish the project directly to the mapped folder
                dotnet publish --configuration Release --output ${DEPLOY_PATH}
                """
            }
        }
        stage('Print Publish Location') {
            steps {
                sh 'echo "Published files are located in: ${DEPLOY_PATH}"'
                sh 'ls -l ${DEPLOY_PATH}'
            }
        }
    }

    post {
        always {
            // Clean up the workspace inside Jenkins
            cleanWs()
        }
    }
}
