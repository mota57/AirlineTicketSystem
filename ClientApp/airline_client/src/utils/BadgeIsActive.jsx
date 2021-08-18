export default function BadgeIsActive ({isActive}) {
    return (
        <div>
        {isActive ? 
            (<h4>  <span className="badge bg-primary">Activo</span></h4>) : 
            (<h4>  <span className="badge bg-secondary">Inactivo</span></h4>)
        }
        </div>
    )
}