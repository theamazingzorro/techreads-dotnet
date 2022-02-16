// Jenkins pipeline file 

pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building..'
                dotnetBuild 'TechReads.sln'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                dotnetTest 'TechReads.sln'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
