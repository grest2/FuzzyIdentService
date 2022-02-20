import React from 'react'
import ReactDOM from 'react-dom'


class User extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {data: props.user};
        
    }
    
    render() {
        return <div>
            <p><b>Имя { this.state.data.FirstName }</b></p>
            <p>Фамилия { this.state.data.LastName }</p>
        </div>
    }
}

class UserList extends React.Component {
    
    constructor(props) {
        super(props);
        
        this.state = { users: []};
    }
    
    loadData() {
        var xhr = XMLHttpRequest();
        
        xhr.open("get",this.props.apiUrl,true);
        
        xhr.onLoad = function () {
            var data = JSON.parse(xhr.responseText);
            
            this.setState({ users: data});
        }.bind(this);
        xhr.send();
    }
    
    componentDidMount() {
        this.loadData();
    }
    render() {
        <div>
            <h2>Список пользователей</h2>   
            <div>
                {
                    this.state.users.map(function (user){ 
                        return <User key = {user.id} user = { user } />
                    })
                    
                }  
            </div>
        </div>
    }
}

ReactDOM.render(
    <MainScreen apiUrl = "api/userscontroller/users/getall"/>,
    document.getElementById("content")
);