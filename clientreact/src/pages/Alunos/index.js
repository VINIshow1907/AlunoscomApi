import React from "react";
import './styles.css';
import { Link } from "react-router-dom";
import logoCadastro from '../../assets/cadastro1.svg';
import { FiUserX, FiXCircle,FiEdit } from "react-icons/fi";
export default function Alunos() {
    return(
        <div className="aluno-container">
            <header>
                <img src={logoCadastro} alt="Cadastro"/>
                <span>Bem-vindo, <strong>Macoratti</strong>!</span>
                <Link className="button" to="aluno/novo">Novo Aluno</Link>
                <button type="button">
                    <FiXCircle size={35} color="#17202a"/>
                </button>
            </header>
           <form>
            <input type="text" placeholder="Nome"/>
            <button type="button" class='button'>
                Filtrar aluno por nome (parcial)
            </button>
           </form> 
           <h1>Relação de Alunos</h1>
           <ul>
            <li>
                <b>Nome:</b>Paulo<br/><b/>
                <b>Email:</b>paulo@email.com<br/><b/>
                <b>Idade:</b>22<br/><b/>
                <button type="button">
                    <FiEdit size="25" color="#17202a" />
                </button>
                <button type="button">
                    <FiUserX size="25" color="#17202a" />
                </button>

            </li>
            <li>
                <b>Nome:</b>Paulo<br/><b/>
                <b>Email:</b>paulo@email.com<br/><b/>
                <b>Idade:</b>22<br/><b/>
                <button type="button">
                    <FiEdit size="25" color="#17202a" />
                </button>
                <button type="button">
                    <FiUserX size="25" color="#17202a" />
                </button>

            </li>
           </ul>
        </div>
    );
}