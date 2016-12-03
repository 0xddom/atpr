using System;


namespace ATPRNER
{
	/// <summary>
	/// Represents the matched entitiy with the information needed.
	/// </summary>
	public class MatchedEntity
	{
		/// <summary>
		/// Gets or sets the name of the entity.
		/// </summary>
		/// <value>The name of the entity.</value>
		public String EntityName {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the match number.
		/// </summary>
		/// <value>The match number.</value>
		public int MatchNumber {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public String Type{
			get;
			set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ATPRNER.MatchedEntity"/> class.
		/// </summary>
		/// <param name="entityName">Entity name.</param>
		public MatchedEntity (String entityName, String type)
		{
			this.EntityName = entityName;
			this.Type = type;
			MatchNumber = 1;
		}

		/// <summary>
		/// Increments the match number one unity.
		/// </summary>
		public void IncrementMatch ()
		{
			MatchNumber++;
		}
	}
}

