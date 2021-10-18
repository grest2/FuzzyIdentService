import React from 'react'
import ReactDOM from 'react-dom'


class MainScreen extends React.component {
    constructor(props) {
        super(props);
        this.state = {users: []};
        this.users = this.loadDataFromApi();
    }

    async loadDataFromApi() {
        try {
            let response = await fetch(this.props.apiUrl);
            let responseJson = await response.json();
            return responseJson.users;
        } catch(ex) {
            console.log(ex);
        }
    }

    render() {
        return <div>
            <h2>Список пользователей</h2>
            <div>
                {
                    this.state.users.map(function (user) {
                        return <h3>user.id</h3>
                    })
                }
            </div>
        </div>
    }
}

ReactDOM.render(
    <MainScreen apiUrl = "api/userscontroller"/>,
    document.getElementById("content")
);