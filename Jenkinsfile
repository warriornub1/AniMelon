pipeline {
    agent any

    environment {
        DOCKER_HOST = 'unix:///var/run/docker.sock'
    }

    stages {
        stage('Test Docker Access') {
            steps {
                sh 'docker info'
            }
        }
    }
}
